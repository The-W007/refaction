using System.Collections.Generic;

namespace refactor_me.Models
{
    
    public class CollectionsDto<T> where T: BaseDto
    {
        public List<T> Items { get; set; }
    }
    
}