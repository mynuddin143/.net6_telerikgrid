using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class User
    {
        public User()
        {
            Ipsfiles = new HashSet<Ipsfile>();
            LoginHistories = new HashSet<LoginHistory>();
            PriceChangeRequestItems = new HashSet<PriceChangeRequestItem>();
            PriceChangeRequests = new HashSet<PriceChangeRequest>();
            SapcustomerGroupRegionalManagerUsers = new HashSet<SapcustomerGroup>();
            SapcustomerGroupUsers = new HashSet<SapcustomerGroup>();
            SapcustomerGroupUsersNavigation = new HashSet<SapcustomerGroupUser>();
            SapcustomerServiceReps = new HashSet<SapcustomerServiceRep>();
            SapsalesGroupAlternateIsrNavigations = new HashSet<SapsalesGroup>();
            SapsalesGroupUsers = new HashSet<SapsalesGroup>();
            SapsalesOrders = new HashSet<SapsalesOrder>();
            ShippingCarts = new HashSet<ShippingCart>();
            ShoppingCarts = new HashSet<ShoppingCart>();
            UserProfiles = new HashSet<UserProfile>();
            WebReleases = new HashSet<WebRelease>();
            SapshipTos = new HashSet<SapshipTo>();
        }

        public long UserId { get; set; }
        public string LastName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string PasswordSalt { get; set; } = null!;
        public bool PasswordReset { get; set; }
        public string Email { get; set; } = null!;
        public DateTime? LastLoginDate { get; set; }
        public bool? Active { get; set; }
        public string? PhoneNumber { get; set; }
        public string? FaxNumber { get; set; }
        public string? Extension { get; set; }
        public string? UserName { get; set; }
        public long? PrimarySapsoldToId { get; set; }

        public virtual SapsoldTo? PrimarySapsoldTo { get; set; }
        public virtual Employee Employee { get; set; } = null!;
        public virtual PlantComputer PlantComputer { get; set; } = null!;
        public virtual ICollection<Ipsfile> Ipsfiles { get; set; }
        public virtual ICollection<LoginHistory> LoginHistories { get; set; }
        public virtual ICollection<PriceChangeRequestItem> PriceChangeRequestItems { get; set; }
        public virtual ICollection<PriceChangeRequest> PriceChangeRequests { get; set; }
        public virtual ICollection<SapcustomerGroup> SapcustomerGroupRegionalManagerUsers { get; set; }
        public virtual ICollection<SapcustomerGroup> SapcustomerGroupUsers { get; set; }
        public virtual ICollection<SapcustomerGroupUser> SapcustomerGroupUsersNavigation { get; set; }
        public virtual ICollection<SapcustomerServiceRep> SapcustomerServiceReps { get; set; }
        public virtual ICollection<SapsalesGroup> SapsalesGroupAlternateIsrNavigations { get; set; }
        public virtual ICollection<SapsalesGroup> SapsalesGroupUsers { get; set; }
        public virtual ICollection<SapsalesOrder> SapsalesOrders { get; set; }
        public virtual ICollection<ShippingCart> ShippingCarts { get; set; }
        public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; }
        public virtual ICollection<UserProfile> UserProfiles { get; set; }
        public virtual ICollection<WebRelease> WebReleases { get; set; }

        public virtual ICollection<SapshipTo> SapshipTos { get; set; }
    }
}
