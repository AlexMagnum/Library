//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Library_bfk
{
    using System;
    using System.Collections.Generic;
    
    public partial class books_students
    {
        public long book_id { get; set; }
        public long student_id { get; set; }
        public System.DateTime date_issue { get; set; }
        public long id { get; set; }
    
        public virtual book book { get; set; }
        public virtual stud stud { get; set; }
    }
}
