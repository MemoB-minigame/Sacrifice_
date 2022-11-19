using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
namespace Kurisu.TimeControl
{
    /// <summary>
    /// 时间回溯控制器
    /// </summary>
    public class TimeController : MonoBehaviour
    {
        public enum TimeState
        {
            正常, 记录, 回溯
        }
        [SerializeField]
        private List<TimeStore> stores = new List<TimeStore>();
        private TimeStore playerStore;
        [SerializeField]
        private TimeState state;
        public TimeState CurrentState
        {
            get { return state; }
        }
        /// <summary>
        /// 为所有回溯器预设容量,你可以测试内存占用后提高上限，因为动态扩容会1.5倍增加容量带来浪费,尽量不要在游戏时扩容
        /// </summary>
        [SerializeField]
        private int capacity = 3000;
        public int Capacity
        {
            get { return capacity; }
        }
        [SerializeField]
        private int currentCount;
        /// <summary>
        /// 记录步长,因为使用Update记录会有更大误差，建议使用FixedUpdate记录数据，FixedDeltaTime默认为0.02f
        /// </summary>
        [SerializeField, Range(0.01f, 0.2f), Tooltip("每多少秒记录一次")]
        private float recordStep = 0.02f;
        [SerializeField, Range(0.01f, 0.2f), Tooltip("每多少秒回溯一次")]
        private float recallStep = 0.02f;

        public int baseRecallBoost = 1;
        public bool autoBoost = false;
        public float recallDuration = 5f;
        [SerializeField] private int adaptiveRecallBoost = 0;
        [SerializeField] private int recallCount;

        public int GetRecallBoost()
        {
            return adaptiveRecallBoost;
        }

        /// <summary>
        /// 当前记录步长
        /// </summary>
        /// <value></value>
        public float RecordStep
        {
            get { return recordStep; }
        }
        private float timer;
        public event Action OnRecordStartEvent;
        /// <summary>
        /// 回溯开始事件
        /// </summary>
        public event Action OnRecallStartEvent;
        /// <summary>
        /// 回溯结束事件
        /// </summary>
        public event Action OnRecallEndEvent;
        /// <summary>
        /// 记录中事件
        /// </summary>
        public event Action OnRecordEvent;
        /// <summary>
        /// 回溯中事件
        /// </summary>
        public event Action OnRecallEvent;
        /// <summary>
        /// 记录数变更事件（适用于UI更新）
        /// </summary>
        public event Action<float> OnStepChangeEvent;
        /// <summary>
        /// 控制器状态变更事件（适用于多人游戏状态同步）
        /// </summary>
        public event Action<TimeState> OnStateChangeEvent;

        [SerializeField]
        private bool useFixedUpdate = true;
        /// <summary>
        /// 使用物理更新FixedUpdateMode
        /// </summary>
        /// <value></value>
        public bool UseFixedUpdate
        {
            get { return useFixedUpdate; }
        }
        public static TimeController instance;
        public static TimeController Instance
        {
            get { return instance; }
        }
        protected virtual void Awake()
        {
            if (instance != null)
                Destroy(gameObject);
            else
                instance = (TimeController)this;
        }
        public static bool IsInitialized
        {
            get { return instance != null; }
        }
        protected virtual void OnDestroy()
        {
            if (instance == this)
            {
                instance = null;
            }
        }
        public void Add(TimeStore store)
        {
            if (!stores.Contains(store))
            {
                stores.Add(store);
            }
        }
        public void Remove(TimeStore store)
        {
            if (stores.Contains(store))
            {
                stores.Remove(store);
            }
        }
        /// <summary>
        /// 回溯器开始记录
        /// </summary>
        public void RecordAll()
        {
            timer = recordStep;
            UpdateState(TimeState.记录);
            OnRecordStartEvent?.Invoke();
            foreach (var store in stores)
            {
                store.Record();
            }

        }
        /// <summary>
        /// 回溯器开始回溯
        /// </summary>
        public void RecallAll()
        {
            timer = 0;
            UpdateState(TimeState.回溯);
            OnRecallStartEvent?.Invoke();
            foreach (var store in stores)
            {
                store.Recall();
            }
            recallCount = currentCount;
        }
        /// <summary>
        /// 强制关闭所有回溯器
        /// </summary>
        public void ShutdownAll()
        {
            state = TimeState.正常;
            currentCount = 0;
            foreach (var store in stores)
            {
                store.ShutDown();
            }
        }
        private void Update()
        {
            if (!useFixedUpdate)
                UpdateStore(Time.deltaTime);
        }
        private void FixedUpdate()
        {
            if (useFixedUpdate)
                UpdateStore(Time.fixedDeltaTime);
        }
        void UpdateStore(float deltaTime)
        {
            switch (state)
            {
                case TimeState.正常:
                    {
                        break;
                    }
                case TimeState.记录:
                    {
                        if (currentCount >= capacity)//到达上限直接回溯
                        {
                            RecallAll();
                            break;
                        }
                        timer += deltaTime;
                        if (timer >= recordStep)
                        {
                            timer = 0;
                            currentCount += 1;
                            OnRecordEvent?.Invoke();//调用记录时刻
                            OnStepChangeEvent?.Invoke((float)currentCount / Capacity);
                        }
                        break;
                    }
                case TimeState.回溯:
                    {
                        if (currentCount == 0 || stores[0].steps.Count==0)//回溯结束调用回溯结束事件
                        {
                            OnRecallEndEvent?.Invoke();
                            UpdateState(TimeState.正常);
                            break;
                        }
                        timer += Time.deltaTime;
                        if (timer >= recallStep)
                        {
                            timer = 0;
                            currentCount -= CalculateRecallBoost(deltaTime);
                            if (currentCount < 0) currentCount = 0;
                            OnStepChangeEvent?.Invoke((float)currentCount / Capacity);
                            OnRecallEvent?.Invoke();//未结束则调用回溯时刻
                        }
                        break;
                    }
            }
        }
        void UpdateState(TimeState newState)
        {
            state = newState;
            OnStateChangeEvent?.Invoke(state);
        }

        int CalculateRecallBoost(float deltaTime)
        {
            adaptiveRecallBoost = (int)(recallCount * recallStep / recallDuration);
            if (!autoBoost || (autoBoost && baseRecallBoost > adaptiveRecallBoost))
            {
                adaptiveRecallBoost = baseRecallBoost;
            }
            int multiplier = (int)(deltaTime / recallStep);
            adaptiveRecallBoost *= multiplier;
            return adaptiveRecallBoost;
        }
    }
}