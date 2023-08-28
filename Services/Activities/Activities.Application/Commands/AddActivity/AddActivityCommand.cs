
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Framework.Core.Messages;
using Activities.Domain.Enums;

namespace Activities.Application.Commands.AddActivity
{
    public class AddActivityCommand : Command<AddActivityCommandOutput>
    {

        /// <summary>
        /// Types of activities\
        /// 1 - Build component: Which is always performed by one worker.\
        /// 2 - Build machine: That can be performed by one or several workers that join together in a team.
        /// </summary>
        /// <example>1</example>
        [Required]
        public TypeActivityBuild TypeActivityBuild { get; set; }
        [Required]
        public DateTime TimeActivityStart { get; set; }
        [Required]
        public DateTime TimeActivityEnd { get; set; }
        [Required]
        // [RegularExpression(@"^[A-Za-z]$"), StringLength(1)]
        public List<string> Workers { get; set; }

        public AddActivityCommand(TypeActivityBuild typeActivityBuild, DateTime timeActivityStart, DateTime timeActivityEnd, List<string> workers)
        {
            this.TypeActivityBuild = typeActivityBuild;
            this.TimeActivityStart = timeActivityStart;
            this.TimeActivityEnd = timeActivityEnd;
            this.Workers = workers.Select(x=>x.ToUpper()).ToList();

            ValidCommand(new AddActivityCommandValidation().Validate(this));
        }
    }
}
