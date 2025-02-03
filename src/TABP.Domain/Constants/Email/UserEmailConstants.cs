namespace TABP.Domain.Constants.Email;

public static class UserEmailConstants
{
    public const string Subject = "Welcome to TABP – We're Glad You're Here";
    public const string Body = @"
+                Hi {user.FirstName},
+                Welcome to TABP. We’re excited to have you join us.
+                At TABP, we are committed to providing a seamless Hotel Booking experience.
+                We understand that every traveler has unique needs and preferences,
+                and we strive to make your experience as smooth and enjoyable as possible.
+                we’re here to support you every step of the way.
+                What’s next?
+                - Get started by exploring your account.
+                - If you need any assistance, our team is ready to help.
+
+                Best regards, 
+                The TABP Team";
}