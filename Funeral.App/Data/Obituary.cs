using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Funeral.App.Data
{
    public class Obituary
    {
        public Obituary()
        {
            Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }

        public string UserId { get; set; }

        public string FrameId { get; set; }

        public virtual Frame Frame { get; set; }

        public string TextTemplateId { get; set; }

        public virtual TextTemplate TextTemplate { get; set; }

        public string CustomTextId { get; set; }

        public virtual CustomText CustomText { get; set; }

        public string CrossId { get; set; }

        public virtual Cross Cross { get; set; }

        public string CrossTextId { get; set; }

        public virtual CrossText CrossTexts { get; set; }

        public string AfterCrossTextId { get; set; }

        public virtual AfterCrossText AfterCrossTexts { get; set; }

        public string FullNameId { get; set; }

        public virtual FullName FullNames { get; set; }

        public string YearId { get; set; }

        public virtual Year Years { get; set; }

        public string PanahidaId { get; set; }

        public virtual Panahida Panahidas { get; set; }

        public string FromId { get; set; }

        public virtual From Froms { get; set; }

        public string PictureId { get; set; }

        public virtual Picture Picture { get; set; }
    }
}
