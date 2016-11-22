package be.pxl.emotion.deadlydragon;

import be.pxl.emotion.Application;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.test.context.jdbc.Sql;
import org.springframework.test.context.jdbc.Sql.ExecutionPhase;
import org.springframework.test.context.jdbc.SqlGroup;
import org.springframework.test.context.junit4.SpringJUnit4ClassRunner;
import org.springframework.test.context.web.WebAppConfiguration;

/**
 * Created by Dragonites on 22/11/2016.
 */
@RunWith(SpringJUnit4ClassRunner.class)
@SpringBootTest(classes = Application.class)
@SpringBootApplication
@SqlGroup(
		{
			@Sql(executionPhase = ExecutionPhase.BEFORE_TEST_METHOD , scripts = "classpath:emotiondb_v4.1_Before.sql"),
			@Sql(executionPhase = ExecutionPhase.AFTER_TEST_METHOD, scripts = "classpath:emotiondb_v4.1_After.sql")
		})


public class ApplicationTests {

    @Test
    public void contextLoads() {

    }
}