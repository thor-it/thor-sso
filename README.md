# thor-sso
FOSS IdentityServer4 solution for single-sign-on of the rugby club SRC Thor.

# Created with
Created with the ABP CLI. Of course this is not production-ready, as we use MySQL:
```abp new Thor.SSO --template app --ui angular --connection-string "Server=host.docker.internal,1433;Database=ThorSSO;User ID=sa;Password=Your_password123;Trusted_Connection=false;"```