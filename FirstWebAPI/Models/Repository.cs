namespace FirstWebAPI.Models
{
    public class Repository
    {

        public static List<Student> Students { get; set; }= new List<Student>(){
                new Student()
                {
                    Id = 1,
                    Name="Mother Fucker",
                    Email="Email1",
                    Address="Heaven"

                },
                new Student()
                {   Id = 2,
                    Name="Bitch",
                    Email="Email2",
                    Address="Hell"

                }
            };
    }
}
