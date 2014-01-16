using System;

namespace Model
{
    //public sealed class Student : BusinessBase<StudentEntity>
    //{
    //    public override OID Add(StudentEntity entity)
    //    {
    //        //entity.Id = DateTime.Now.ToString("yyyyMMddHHmmssfff");
    //        //entity.Oid = OIDFactory.BuildClassOID(3);
    //        return base.Add(entity);
    //    }
    //}

    public class StudentEntity
    {
        public string Id
        {
            get;
            set;
        }

        public int Age { get; set; }

        public string Name { get; set; }

        public string Tel { get; set; }

        public string Address { get; set; }

        public DateTime CreateDate
        {
            get { return DateTime.Now; }
            set { CreateDate = value; }
        }
    }
}
