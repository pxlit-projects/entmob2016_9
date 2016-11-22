package be.pxl.emotion.deadlydragon;

import be.pxl.emotion.beans.User;
import be.pxl.emotion.repositories.UserRepository;
import org.hamcrest.beans.HasProperty;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.test.context.ContextConfiguration;
import org.springframework.test.context.TestExecutionListeners;
import org.springframework.test.context.junit4.SpringJUnit4ClassRunner;
import org.springframework.test.context.support.DependencyInjectionTestExecutionListener;
import org.springframework.test.context.support.DirtiesContextTestExecutionListener;
import org.springframework.test.context.transaction.TransactionalTestExecutionListener;

import java.util.List;

import static org.junit.Assert.assertThat;
import static org.junit.Assert.assertTrue;

@RunWith(SpringJUnit4ClassRunner.class)
@ContextConfiguration(classes ={PersistenceContext.class})

@TestExecutionListeners({
	DependencyInjectionTestExecutionListener.class,
	DirtiesContextTestExecutionListener.class,
        TransactionalTestExecutionListener.class
 //       , DBUnitTestExecutionListener.class
})

//@DataSetup("ToDo.xml")
public class UserRepositoryTest {
	
	@Autowired
	private UserRepository repository;
	
	@Test
	public void search_DataBaseEntriesFound_ShouldReturnAList()
	{
		long todoEntries = repository.count();
		boolean check = false;
		if (todoEntries > 0)
		{
			check = true;
		}
		assertTrue(check);
	}
	
	@Test
	public void search_OneUserFound_ShouldReturnAListOfOneEntry()
	{
		List<User> userEntries = repository.findByUserName("Giel");
		
		/*assertThat(userEntries.size(),is(1));
		
		assertThat(userEntries.get(0), allOf(
				HasProperty("id",is(1)),
				HasProperty("firstname", is("Giel")),
				HasProperty("lastname", is("Reynders"))
				));*/
	}
}
