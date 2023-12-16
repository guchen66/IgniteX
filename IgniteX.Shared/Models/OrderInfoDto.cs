using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IgniteX.Shared.Models
{
    public class OrderInfoDto : BindableBase
    {
        /// <summary>
        /// 订单Id
        /// </summary>
        private int _id;
        public int Id
        {
            get { return _id; }
            set { SetProperty<int>(ref _id, value); }
        }

        /// <summary>
        /// 订单名称
        /// </summary>
        private string _orderName;
        public string OrderName
        {
            get { return _orderName; }
            set { SetProperty<string>(ref _orderName, value); }
        }


    }
}
