using FirstWebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FirstWebAPI.Controllers
{
  
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController :ControllerBase


    {
        private readonly ILogger<StudentController> _logger;

        public StudentController(ILogger<StudentController> logger)
        {
            _logger=logger;
        }


        [HttpGet]
        [Route("All",Name ="GetAllStudent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public  ActionResult<IEnumerable<StudentDTO>> getStudents()
        {
            //List<StudentDTO> studentDto=new List<StudentDTO>();
            //foreach(Student student in Repository.Students)
            //{
            //    StudentDTO objDto=new StudentDTO()
            //    {
            //        Id = student.Id,
            //        Name = student.Name,
            //        Address = student.Address,
            //        Email = student.Email,

            //    };

            //}
            //studentDto.Add(objDto);
            //Linq 

            _logger.LogInformation("Get Student Method Start");
            List<StudentDTO> studentDto = Repository.Students.Select(student => new StudentDTO()
                {
                    Id = student.Id,
                    Name = student.Name,
                    Address = student.Address,
                    Email = student.Email,
                    RegisterDate = student.RegisterDate,


                }).ToList();

            return Ok(studentDto);


        }
        //[HttpGet("{id:int}")]
        [Authorize]
        [HttpGet]
        [Route("{id:int}", Name = "GetStudentByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StudentDTO> getStudentByID(int id)
        {
            // BadRequest - 400 -Clinet Error
            if (id < 0)
            {
                _logger.LogWarning("Bad Request");
                return BadRequest();

            }
              

            // NotFound - 404 -Clinet Error
            Student student = Repository.Students.Where(c => c.Id == id).FirstOrDefault();
            if(student == null)
            {
                _logger.LogError("Sudent is not found with given ID");
                return NotFound($"The student with ID : {id} not Found");
            }
               

            StudentDTO studentDTO = new StudentDTO()
            {
                Id = student.Id,
                Name = student.Name,
                Address = student.Address,
                Email = student.Email,
                RegisterDate = student.RegisterDate,

            };

            //OK -200 -Success
            return Ok(studentDTO);


        }
        
        [HttpGet("{name:Alpha}", Name = "GetStudentByName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StudentDTO> getStudentByID(string name)
        {

            // BadRequest - 400 -Clinet Error
            if (String.IsNullOrEmpty(name))
                return BadRequest();

            // NotFound - 404 -Clinet Error
            Student student = Repository.Students.Where(c=>c.Name.ToLower()== name.ToLower()).FirstOrDefault();
            if (student == null)
                return NotFound($"The student with Name : {name} not Found");
            StudentDTO studentDTO = new StudentDTO()
            {
                Id = student.Id,
                Name = student.Name,
                Address = student.Address,
                Email = student.Email,
                RegisterDate = student.RegisterDate,

            };
            //OK -200 -Success
            return Ok(studentDTO);

        } 
        [HttpDelete("{id}", Name = "DeleteStudentByName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<bool> DeleteStudent(int id)
        {
            if (id < 0)
                return BadRequest();

            Student student = Repository.Students.Where(c => c.Id == id).FirstOrDefault();
            if (student == null)
                return NotFound($"The student with ID : {id} not Found");

            Repository.Students.Remove(student);
            return Ok();


        }

     
        [HttpPost("Create", Name = "CreatStudent")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StudentDTO> CreatStudent([FromBody]StudentDTO model)
        {
            //If Don't Use [ApiController],Use This code to Validate Input Model
            //if (!ModelState.IsValid)
            //    return BadRequest(ModelState);


            if (model ==null)
                return BadRequest();


            

            var newID = Repository.Students.LastOrDefault().Id + 1;

            Student newstudentDTO = new Student()
            {
                Id = newID,
                Name = model.Name,
                Address = model.Address,
                Email = model.Email,
                RegisterDate = model.RegisterDate,
            };
            Repository.Students.Add(newstudentDTO);
            model.Id = newID;

            //201-Created
            //https://localhost:7176/api/Student/3


            return CreatedAtRoute("GetStudentByID", new { id = model.Id }, model);


        }
        [HttpPut("Update", Name = "UpdateStudent")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult UpdateStudent([FromBody] StudentDTO model)
        {
            //If Don't Use [ApiController],Use This code to Validate Input Model
            //if (!ModelState.IsValid)
            //    return BadRequest(ModelState);


            if (model.Id == null || model.Id <0)
                return BadRequest();




            var OldStudent = Repository.Students.Where(c=> c.Id == model.Id).FirstOrDefault();
            if(OldStudent==null)
                return NotFound();

            OldStudent.Name=model.Name;
            OldStudent.Address=model.Address;
            OldStudent.Email =model.Email;
            OldStudent.RegisterDate = model.RegisterDate;
          


            return NoContent();


        }
        [HttpPatch("{id:int}UpdatePartialy", Name = "UpdateStudentPartialy")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult UpdateStudentPartialy( int id,[FromBody] JsonPatchDocument<StudentDTO> PatchDocument)
        {
            //If Don't Use [ApiController],Use This code to Validate Input Model
            //if (!ModelState.IsValid)
            //    return BadRequest(ModelState);


            if (PatchDocument == null || id <0)
                return BadRequest();




            var OldStudent = Repository.Students.Where(c=> c.Id == id).FirstOrDefault();
            if(OldStudent==null)
                return NotFound();


            var studenDto = new StudentDTO()
            {
                Id=OldStudent.Id,
                Name = OldStudent.Name,
                Address = OldStudent.Address,
                Email = OldStudent.Email,
                RegisterDate = OldStudent.RegisterDate,

            };
            PatchDocument.ApplyTo(studenDto,ModelState);
            if(!ModelState.IsValid) 
                return BadRequest(ModelState);


            OldStudent.Name= studenDto.Name;
            OldStudent.Address= studenDto.Address;
            OldStudent.Email = studenDto.Email;
            OldStudent.RegisterDate = studenDto.RegisterDate;
          


            return NoContent();


        }
    }
}
