package com.pxl.emotionjava.config;

import org.apache.activemq.ActiveMQConnectionFactory;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.jms.annotation.EnableJms;

import javax.jms.ConnectionFactory;

@Configuration
@EnableJms
public class MessagingConfig {
    private static final String JMS_BROKER_URL = "vm://embedded?broker.persistent=false,useShutdownHook=false";

    @Bean
    public ConnectionFactory connectionFactory() {
        return new ActiveMQConnectionFactory(JMS_BROKER_URL);
    }
}
