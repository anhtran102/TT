using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tieptuyen.Common
{
    class MyGridAggregateFunction
    {
        string aggregateName;

        public string AggregateName
        {
            get { return aggregateName; }
            set { aggregateName = value; }
        }
        int aggregateValue;

        public int AggregateValue
        {
            get { return aggregateValue; }
            set { aggregateValue = value; }
        }

        public MyGridAggregateFunction(string name,int value)
        {
            aggregateName = name;
            aggregateValue = value;
        }

        public static List<MyGridAggregateFunction> GetList(){
            List<MyGridAggregateFunction> list = new List<MyGridAggregateFunction>();
            list.Add(new MyGridAggregateFunction("Số lượng", 6));
            list.Add(new MyGridAggregateFunction("Trung bình", 7));
            list.Add(new MyGridAggregateFunction("Lớn nhất", 3));
            list.Add(new MyGridAggregateFunction("Nhỏ nhất", 2));
            list.Add(new MyGridAggregateFunction("Tổng", 1));

            return list;
        }
    }
}
