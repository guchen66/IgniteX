using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive;

namespace IgniteX.TransferService
{
    /// <summary>
    /// ObserverBase 是 System.Reactive 类库中的一个抽象基类，用于实现观察者模式。
    /// OnNext(T value)：当观察到可观察序列中的新元素时调用该方法。你可以在继承类中重写该方法，以执行特定的操作来处理接收到的数据。
    /// OnError(Exception error)：当可观察序列中出现错误时调用该方法。你可以在继承类中重写该方法，以执行特定的操作来处理错误情况
    /// OnCompleted()：当可观察序列已完成时调用该方法。你可以在继承类中重写该方法，以执行特定的操作来处理序列完成的情况。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class ObserverList<T> : ObserverBase<T>
    {
        private readonly List<IObserver<T>> _observers = new List<IObserver<T>>();

        public void Add(IObserver<T> observer) => _observers.Add(observer);

        public void Remove(IObserver<T> observer) => _observers.Remove(observer);

        protected override void OnNextCore(T value)
        {
            ForEachObserver(item => item.OnNext(value));
        }

        protected override void OnErrorCore(Exception error)
        {
            ForEachObserver(item => item.OnError(error));
        }

        protected override void OnCompletedCore()
        {
            ForEachObserver(item => item.OnCompleted());
        }

        private void ForEachObserver(Action<IObserver<T>> callback)
        {
            ForEach(_observers, callback);
        }

        /// <summary>
        /// 相当于传入一个List，然后回调它
        /// </summary>
        /// <typeparam name="TItem"></typeparam>
        /// <param name="list"></param>
        /// <param name="callback"></param>
        private static void ForEach<TItem>(ICollection<TItem> list, Action<TItem> callback)
        {
            var backup = new TItem[list.Count];       //new一个对象，对象长度是集合长度
            list.CopyTo(backup, 0);                    //从0开始复制

            foreach (var item in backup)
            {
                callback?.Invoke(item);
            }
        }
    }
}
