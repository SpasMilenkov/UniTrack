namespace UniTrackBackend.Data.Models.TypeSafe;

public abstract class Ts
{
    
    public static class Roles
    {
        public const string SuperAdmin = "SuperAdmin";
        public const string Admin = "Admin";
        public const string Teacher = "Teacher";
        public const string Parent = "Parent";
        public const string Student = "Student";
        public const string Guest = "Guest";
    }
    
    public static class Policies
    {
        public const string ReadPolicy = "ReadPolicy";
        public const string ReadAndWritePolicy = "AddAndReadPolicy";
        public const string FullControlPolicy = "FullControlPolicy";

        public const string GenericPolicy = "GenericPolicy";
    }
}