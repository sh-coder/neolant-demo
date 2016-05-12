call config.cmd

bcp PROPERTY_KIND format nul -f PROPERTY_KIND.fmt -n %PARAMS%
bcp FACILITY_CLASS format nul -f FACILITY_CLASS.fmt -n %PARAMS%
bcp COMMON_UNIVERSAL_PROPERTY format nul -f COMMON_UNIVERSAL_PROPERTY.fmt -n %PARAMS%
bcp FACILITY format nul -f FACILITY.fmt -n %PARAMS%
bcp PROPERTY format nul -f PROPERTY.fmt -n %PARAMS%

@pause