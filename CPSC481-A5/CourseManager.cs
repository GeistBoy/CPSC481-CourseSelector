using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPSC481_A5
{
    class CourseManager
    {
        List<Course> taken;
        List<Course> current;

        public CourseManager(List<Course> taken, List<Course> current)
        {
            this.taken = new List<Course>(taken);
            this.current = new List<Course>(current);
        }
    }
}
