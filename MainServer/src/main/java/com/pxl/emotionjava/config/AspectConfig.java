package com.pxl.emotionjava.config;

import com.pxl.emotionjava.services.ErrorAspect;
import com.pxl.emotionjava.services.LogAspect;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.context.annotation.EnableAspectJAutoProxy;

@Configuration
@EnableAspectJAutoProxy(proxyTargetClass = true)
public class AspectConfig {

    @Bean
    public LogAspect logAspect() {
        return new LogAspect();
    }

    @Bean
    public ErrorAspect errorAspect() {
        return new ErrorAspect();
    }
}
