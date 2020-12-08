using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

#nullable disable

namespace WebCoreKino5.Models
{
    public partial class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        public string Surname { get; set; }
        [Required]
        [StringLength(50)]
        public string Mail { get; set; }
        [Required]
        [StringLength(50)]
        public string Password { get; set; }
        [StringLength(50)]
        public string KeyWord { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ExpiredDate { get; set; }
        public bool? IsTemporary { get; set; }
        [Column(TypeName = "image")]
        public byte[] Image { get; set; }
        [MaxLength(50)]

        public byte[] Image2 { get; set; }
        public string Description { get; set; }
        public int RoleId { get; set; }

        [ForeignKey(nameof(RoleId))]
        [InverseProperty("Users")]
        public virtual Role Role { get; set; }
    }
}
