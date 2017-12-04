namespace Task01
{
    using System;
    using System.Linq;

    public class MyArray
    {
        private int[] array;
        private bool isSort;
        private int? maxIndex;
        private int? minIndex;

        public MyArray()
        {
            Random rnd = new Random();
            this.Array = new int[rnd.Next(0, 10)];
            this.IsSort = false;
            int max = int.MinValue;
            int min = int.MaxValue;

            for (var i = 0; i < this.Array.Count(); i++)
            {
                this.Array[i] = rnd.Next(0, 100);

                if (this.Array[i] > max)
                {
                    max = this.Array[i];
                    this.MaxIndex = i;
                }

                if (this.Array[i] < min)
                {
                    min = this.Array[i];
                    this.MinIndex = i;
                }
            }
        }

        public int[] Array
        {
            get
            {
                return this.array;
            }

            set
            {
                this.IsSort = false;
                this.array = value;
            }
        }

        public bool IsSort
        {
            get
            {
                return this.isSort;
            }

            set
            {
                this.isSort = value;
            }
        }

        public int? MaxIndex
        {
            get
            {
                return this.maxIndex;
            }

            set
            {
                this.maxIndex = value;
            }
        }

        public int? MinIndex
        {
            get
            {
                return this.minIndex;
            }

            set
            {
                this.minIndex = value;
            }
        }

        public int? GetMax()
        {
            if (!this.ArrayIsEmpty())
            {
                if (this.MaxIndex != null)
                {
                    return this.MaxIndex;
                }
                else
                {
                    this.MaxIndex = 0;

                    for (var i = 0; i < this.array.Count(); i++)
                    {
                        if (this.array[i] > this.array[(int)this.MaxIndex])
                        {
                            this.MaxIndex = i;
                        }
                    }

                    return this.MaxIndex;
                }
            }

            return null;
        }

        public int? GetMin()
        {
            if (!this.ArrayIsEmpty())
            {
                if (this.MinIndex != null)
                {
                    return this.MinIndex;
                }
                else
                {
                    this.MinIndex = 0;

                    for (var i = 0; i < this.array.Count(); i++)
                    {
                        if (this.array[i] > this.array[(int)this.MinIndex])
                        {
                            this.MinIndex = i;
                        }
                    }

                    return this.MinIndex;
                }
            }

            return null;
        }

        public void Sort()
        {
            if (!this.ArrayIsEmpty())
            {
                this.MergeSort(0, this.array.Count() - 1);
                this.IsSort = true;
                this.MaxIndex = this.array.Count() - 1;
                this.MinIndex = 0;
            }
        }

        private void MergeSort(int left, int right)
        {
            if (left < right)
            {
                int middle = (right + left) / 2;

                this.MergeSort(left, middle);
                this.MergeSort(middle + 1, right);

                this.Merge(left, middle, right);
            }
        }

        private void Merge(int left, int middle, int right)
        {
            int positionLeft = left;
            int positionRight = middle + 1;
            int sizeTmp = right - left + 1;
            var tmp = new int[sizeTmp];
            int positionTmp = 0;

           while (positionLeft <= middle && positionRight <= right)
            {
                if (this.Array[positionLeft] < this.Array[positionRight])
                {
                    tmp[positionTmp] = this.Array[positionLeft];
                    positionLeft++;
                    positionTmp++;
                }
                else
                {
                    tmp[positionTmp] = this.Array[positionRight];
                    positionRight++;
                    positionTmp++;
                }
            }

            while (positionLeft <= middle)
            {
                tmp[positionTmp] = this.Array[positionLeft];
                positionLeft++;
                positionTmp++;
            }

            while (positionRight <= right)
            {
                tmp[positionTmp] = this.Array[positionRight];
                positionRight++;
                positionTmp++;
            }

            for (var i = 0; i < sizeTmp; i++)
            {
                this.Array[left + i] = tmp[i];
            }
        }

        private bool ArrayIsEmpty()
        {
            bool arrayIsEmpty = this.array == null || this.array.Count() == 0;
            return arrayIsEmpty;
        }
    }
}
