namespace ProsjektOppgaveWebAPI.Common;

public static class Errors
{
    public const string GENERATE_JWT_TOKEN_ERROR = "Error while generating JWT token";
    public const string USER_NOT_FOUND_ERROR = "User not found";
    public const string USER_ALREADY_EXISTS_ERROR = "User already exists";
    public const string BLOG_NOT_FOUND_ERROR = "Blog not found";
    public const string BLOG_ALREADY_EXISTS_ERROR = "Blog already exists";
    public const string POST_NOT_FOUND_ERROR = "Post not found";
    public const string POST_ALREADY_EXISTS_ERROR = "Post already exists";
    public const string CANT_CREATE_POST_ERROR = "Can't create post";
    public const string CANT_UPDATE_POST_ERROR = "Can't update post";
}