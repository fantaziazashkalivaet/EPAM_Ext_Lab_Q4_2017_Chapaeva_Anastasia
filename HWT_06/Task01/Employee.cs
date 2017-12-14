namespace Task01
{
    using System;

    public class Employee : User
    {
        private Post post;
        private int workExperience;

        public Employee() : base()
        {
        }

        public Employee(string firstName, string secondName, string patronimic, DateTime dateOfBirth, Post post, int workExp) 
            : base(firstName, secondName, patronimic, dateOfBirth)
        {
            this.post = post;
            this.workExperience = workExp;
        }

        public Post Post
        {
            get
            {
                return post;
            }

            set
            {
                post = value;
            }
        }

        public int WorkExperience
        {
            get
            {
                return workExperience;
            }

            set
            {
                if (value >= 0 && value < 100)
                {
                    workExperience = value;
                }
                else
                {
                    workExperience = 0;
                }
            }
        }
    }
}
