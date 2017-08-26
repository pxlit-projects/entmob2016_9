package com.pxl.emotionjava.config;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.context.annotation.Configuration;
import org.springframework.security.authentication.encoding.ShaPasswordEncoder;
import org.springframework.security.config.annotation.authentication.builders.AuthenticationManagerBuilder;
import org.springframework.security.config.annotation.method.configuration.EnableGlobalMethodSecurity;

import javax.sql.DataSource;

@Configuration
@EnableGlobalMethodSecurity(securedEnabled=true)
public class SecurityConfig{

    @Autowired
    public void configureSecurity(AuthenticationManagerBuilder auth, DataSource ds) throws Exception {
        auth.jdbcAuthentication()
                .passwordEncoder(new ShaPasswordEncoder(256))
                .dataSource(ds)
                .usersByUsernameQuery(
                        "select user_name as username, password, 1 as enabled from public.users where user_name = ?"
                )
                .authoritiesByUsernameQuery(
                        "select u.user_name as username, user_role.role from public.users as u, public.user_role where u.user_name = ? and u.id = user_role.user_id"
                );
    }
}
