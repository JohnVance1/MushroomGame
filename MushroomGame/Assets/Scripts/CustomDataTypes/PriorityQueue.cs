using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriorityQueue
{
    class PriorityQueue
    {
        private List<int> list = new List<int>();

        public List<int> List
        {
            get { return list; }

        }

        public PriorityQueue()
        {

        }

        public void Enqueue(int data)
        {
            list.Add(data);
            double count = list.Count - 1;
            int index = (int)Math.Floor((count - 1) / 2);
            int y = 0;

            if (count > 0)
            {
                while (y == 0)
                {
                    if (list[(int)count] < list[index])
                    {

                        int temp = list[index];
                        list[index] = list[(int)count];
                        list[(int)count] = temp;

                        if (count > 1 && index > 0)
                        {
                            count = index;
                            index = (int)Math.Floor((count - 1) / 2);

                        }

                    }

                    else
                    {
                        y = 1;

                    }
                }
            }
        }

        public int Dequeue()
        {
            int count = list.Count - 1;
            int root;

            if (list.Count == 2 && list[0] > list[1])
            {
                root = list[1];
            }

            else
            {
                root = list[0];
            }

            list[0] = list[count];
            list.RemoveAt(count);
            int y = 0;

            int left = 2 * 0 + 1;
            int right = 2 * 0 + 2;
            int parent = 0;
            while (y == 0)
            {
                left = 2 * parent + 1;
                right = 2 * parent + 2;

                if (list.Count > right)
                {
                    if (list[parent] > list[left] && list[parent] > list[right])
                    {
                        if (list[left] < list[right])
                        {
                            int temp = list[parent];
                            list[parent] = list[left];
                            list[left] = temp;

                            parent = left;

                        }

                        else
                        {
                            int temp = list[parent];
                            list[parent] = list[right];
                            list[right] = temp;

                            parent = right;

                        }

                    }

                    else if (list[parent] > list[left] || list[parent] > list[right])
                    {
                        if (list[parent] > list[left])
                        {
                            int temp = list[parent];
                            list[parent] = list[left];
                            list[left] = temp;

                            parent = left;

                        }

                        else if (list[parent] > list[right])
                        {
                            int temp = list[parent];
                            list[parent] = list[right];
                            list[right] = temp;

                            parent = right;

                        }
                    }

                    else
                    {
                        y = 1;

                    }
                }

                else
                {
                    return root;


                }

            }

            return root;

        }

        public int Peek()
        {
            return list[0];


        }

        public bool IsEmpty()
        {
            if (list.Count == 0)
            {
                return true;

            }

            else
            {
                return false;

            }


        }


    }
}
