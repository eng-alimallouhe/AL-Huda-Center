namespace LMS.Common.Enums
{
    public enum ResponseStatus
    {
        // User ResponseStatus for error: 
        ACCOUNT_NOT_FOUND,
        BLOCKED_USER,
        LOCKED_ACCOUNT,
        MAX_LOGIN_ATTEMPTS,
        UNVERIFIED_ACCOUNT,
        EXPIRED_SESSION,
        EXISTING_ACCOUNT,
        WEAK_PASSWORD,
        AUTHENTICATION_FAILED,
        SAME_PASSWORD,
        ACTIVATION_SUCCESS,
        ACTIVATION_FAILED,
        MORE_STEP_REQUERD,
        UNVALIDE_TOKEN,

        // User ResponseStatus for success: 
        AUTHENTICATION_SUCCESS,


        // OTP Code ResponseStatus for errors: 
        CODE_IS_EXPIRED,
        CODE_NOT_FOUND,
        HIT_MAX_ATTEMPTS,
        FAILED_ATTEMPT,
        CODE_ERROR,
        SUCCESSS_CODE_SEND,
        VERIFY_SUCCESS,
        INDEFINITE_TIME_PERIOD,
        WRONGE_CODE_TYPE,


        // Common ResponseStatus for errors:
        UPDATE_INFORMATION_ERROR,
        ADD_ERROR,
        DELETE_ERROR,
        BACK_ERROR,
        SOURCE_NOT_FOUND,
        FILE_NOT_FOUND,
        TASK_COMPLETED,
        HTTP_RESPONSE_ERROR
    }
}
