using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AADLBusiness;
using FluentValidation;

namespace AADL
{
    internal class PersonValidator:AbstractValidator<clsPerson>
    {
        public PersonValidator() 
        {

            RuleFor(p => p.FirstName).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("الاسم الاول فارغ")
                .Length(3, 17).WithMessage("عدد الحروف للاسم يحب ان يكون اقل شي 3 و اقصى حد 17 حرف ")
                .Must(BeAValidName).WithMessage("الاسم الاول يحتوي على رموز او حروف")
                .Must(BeAUniqueFullName).WithMessage("الاسم الرباعي يحتوي على تطابق مع شخص اخر");

            RuleFor(p => p.SecondName).Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty().WithMessage("الاسم الثاني فارغ")
            .Length(3, 17).WithMessage("\"عدد الحروف للاسم يحب ان يكون اقل شي 3 و اقصى حد 17 حرف \"")
            .Must(BeAValidName).WithMessage("الاسم الثاني يحتوي على رموز او حروف")
            .Must(BeAUniqueFullName).WithMessage("الاسم الرباعي يحتوي على تطابق مع شخص اخر");

            RuleFor(p => p.ThirdName).Cascade(CascadeMode.StopOnFirstFailure)
                        .NotEmpty().WithMessage("الاسم الثالث فارغ")
                        .Length(3, 17).WithMessage("\"عدد الحروف للاسم يحب ان يكون اقل شي 3 و اقصى حد 17 حرف \"")
                        .Must(BeAValidName).WithMessage("الاسم الثالث يحتوي على رموز او حروف")
                        .Must(BeAUniqueFullName).WithMessage("الاسم الرباعي يحتوي على تطابق مع شخص اخر");
            RuleFor(p => p.LastName).Cascade(CascadeMode.StopOnFirstFailure)
                        .NotEmpty().WithMessage("الاسم الرابع فارغ")
                        .Length(3, 17).WithMessage("\"عدد الحروف للاسم يحب ان يكون اقل شي 3 و اقصى حد 17 حرف \"")
                        .Must(BeAValidName).WithMessage("الاسم الرابع يحتوي على رموز او حروف")
                        .Must(BeAUniqueFullName).WithMessage("الاسم الرباعي يحتوي على تطابق مع شخص اخر");
        }
        protected bool BeAValidName(string name)
        {
            name=name.Replace(" ", "");
            name = name.Replace("-", "");
            return name.All(char.IsLetter);
        }

        protected bool BeAUniqueFullName(string FullName)
        {
            return clsPerson.IsFullNameDuplicated(FullName);
        }
    }
}
