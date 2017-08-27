package com.pxl.emotionjava;

import com.pxl.emotionjava.services.ErrorAspect;
import com.pxl.emotionjava.services.LogAspect;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.context.annotation.Bean;

@SpringBootApplication
public class MainServerApplication {

	@Bean
	public LogAspect logAspect() {
		return new LogAspect();
	}

	@Bean
	public ErrorAspect errorAspect() {
		return new ErrorAspect();
	}

	public static void main(String[] args) {
		SpringApplication.run(MainServerApplication.class, args);
	}
}