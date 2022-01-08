using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NEACSwimmingPoolMang.Models.Dtos
{
    public class SignupCourseDtoModel
    {
        public string StuName { get; set; }
        public string StuEmail { get; set; }
        public string StuBirthday { get; set; }
        public string StuSex { get; set; }
        public string StuTarget { get; set; }
        public string ClassName { get; set; }
        public string ClassLevel { get; set; }
        public IEnumerable<ClassTimeModel> ClassTimeName { get; set; }
        public string ClassStartTime { get; set; }
        public string PaymentMethods { get; set; }
    }
    public class ClassTimeModel
    {
        public string Label { get; set; }
        public string Value { get; set; }
        public bool CheckedName { get; set; }

    }



}
