package com.pxl.emotionjava;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.context.annotation.Bean;

@SpringBootApplication
public class MainServerApplication {

	@Bean
	public LogAspect logAspect() {
		return new LogAspect();
	}

	public static void main(String[] args) {
		SpringApplication.run(MainServerApplication.class, args);
	}
}