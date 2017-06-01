Module Settings
    Public debug As Boolean = False


    Public classCTXString As String = "OU=Subject Groups, OU=Security Groups, OU=AS Groups, OU=Ashby School, DC=as, DC=Internal"
    Public usersCTXString As String = "OU=AS Users, OU=Ashby School, DC=as, DC=Internal"
    Public tutorsCTXString As String = "OU=Tutor Groups, OU=Security Groups, OU=AS Groups, OU=Ashby School, DC=as, DC=Internal"
    Public yearCTXString As String = "OU=Distribution Groups, OU=AS Groups, OU=Ashby School, DC=as, DC=Internal"
    Public studentOUPATH As String = "OU=20{0} Students, OU=Students, OU=School Users, OU=AS Users, OU=Ashby School, DC=as, DC=internal"
    Public studentADPath As String = "OU=20{0} Students, OU=Students, OU=School Users, {1}"
    Public tlou As String = "dc=as, dc=intenal"
    Public adUser As String = "ASBromcomADSyncer"
    Public adPass As String = "THw8DCzcPMPwqNC5zBUF"

    Public userHomeDrive As String = "N:"
    Public userPrincipalSuffix As String = "@ashbyschool.org.uk"

    Public domain As String = "as.internal"

    Public sltGroup As String = "AS SLT"
    Public teachingGroup As String = "AS Teaching Staff"
    Public nonteachingGroup As String = "AS Non-Teaching Staff"
    Public studentGroup As String = "AS Students"
    Public domainAdminGroup As String = "Domain Admins"

End Module
