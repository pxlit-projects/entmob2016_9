package be.pxl.emotion.deadlydragon;

import org.junit.runner.RunWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.test.context.ContextConfiguration;
import org.springframework.test.context.TestExecutionListeners;
import org.springframework.test.context.junit4.SpringJUnit4ClassRunner;
import org.springframework.test.context.support.DependencyInjectionTestExecutionListener;
import org.springframework.test.context.support.DirtiesContextTestExecutionListener;

import be.pxl.emotion.repositories.UserRepository;
import ch.qos.logback.core.db.dialect.DBUtil;
import deadlydragon.PersistenceContext;

@RunWith(SpringJUnit4ClassRunner.class)
@ContextConfiguration(classes ={PersistenceContext.class})

@TestExecutionListeners({
	DependencyInjectionTestExecutionListener.class,
	DirtiesContextTestExecutionListener.class,
	DBUtitTestExecutionListener.class })

public class UserRepositoryTest {
	
	@Autowired
	private UserRepository repository;
}
