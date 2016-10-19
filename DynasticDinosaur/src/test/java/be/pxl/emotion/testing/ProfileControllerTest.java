package be.pxl.emotion.testing;

import static org.junit.Assert.*;

import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;

import org.junit.Test;

public class ProfileControllerTest {

	@Test
	public void test() {
		HttpURLConnection connection = null;
		try {
			URL url = new URL("http://localhost:8080/DynasticDinosaur/profile/1");
		} catch (MalformedURLException e) {
			fail("Not yet implemented");
			e.printStackTrace();
		} finally {
		}
	}

}
