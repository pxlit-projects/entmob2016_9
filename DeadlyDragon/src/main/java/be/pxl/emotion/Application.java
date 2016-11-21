package be.pxl.emotion;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.context.ConfigurableApplicationContext;
import org.springframework.context.annotation.Bean;
import org.springframework.jms.annotation.EnableJms;
import org.springframework.jms.support.converter.MappingJackson2MessageConverter;
import org.springframework.jms.support.converter.MessageConverter;
import org.springframework.jms.support.converter.MessageType;
import org.springframework.security.authentication.encoding.ShaPasswordEncoder;
import org.springframework.security.config.annotation.authentication.builders.AuthenticationManagerBuilder;
import org.springframework.security.config.annotation.web.WebSecurityConfigurer;
import org.springframework.security.config.annotation.web.builders.HttpSecurity;
import org.springframework.security.config.annotation.web.builders.WebSecurity;
import org.springframework.security.config.annotation.web.configuration.WebSecurityConfigurerAdapter;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import javax.sql.DataSource;


@RestController
@SpringBootApplication
@EnableJms
public class Application {
	@RequestMapping("/")
	String home() {
		return "I'm a pwetty pwetty pwincess!";
	}

    @Bean
    public WebSecurityConfigurer<WebSecurity> securityConfigurer() {
        return new WebSecurityConfigurerAdapter() {

            @Autowired
            protected void configure(AuthenticationManagerBuilder auth, DataSource ds) throws Exception {
                auth.jdbcAuthentication()
                        .passwordEncoder(new ShaPasswordEncoder(256))
                        .dataSource(ds)
                        .usersByUsernameQuery("SELECT userName, password FROM user WHERE userName = ?")
                        .authoritiesByUsernameQuery("SELECT userName,role FROM users WHERE userName = ?");
            }

            @Override
            protected void configure (HttpSecurity http) throws Exception {
                http.csrf().disable();
                http.httpBasic();
                http.authorizeRequests()
                        .antMatchers("/profile/**").hasAnyRole("BASIC", "ADMIN")
                        .antMatchers("/user/**").hasAnyRole("BASIC", "ADMIN")
                        .antMatchers("/action/**").hasRole("ADMIN")
                        .antMatchers("/command/**").hasRole("ADMIN")
                        .antMatchers("/action/all").hasAnyRole("BASIC", "ADMIN")
                        .antMatchers("/command/all").hasAnyRole("BASIC", "ADMIN");
            }
        };
    }

    @Bean // Serialize message content to json using TextMessage
    public MessageConverter jacksonJmsMessageConverter() {
        MappingJackson2MessageConverter converter = new MappingJackson2MessageConverter();
        converter.setTargetType(MessageType.TEXT);
        converter.setTypeIdPropertyName("_type");
        return converter;
    }

	public static void main(String[] args) {

        ConfigurableApplicationContext context = SpringApplication.run(Application.class, args);
	}
}
