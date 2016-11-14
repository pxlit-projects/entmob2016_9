package be.pxl.emotion;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.context.annotation.ComponentScan;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@SpringBootApplication
public class Application {
	@RequestMapping("/")
	String home() {
		return "I'm a pwetty pwetty pwincess!";
	}

	public static void main(String[] args) {

		SpringApplication.run(Application.class, args);
	}
}
