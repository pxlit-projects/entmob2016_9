package be.pxl.emotion.main;

import java.io.IOException;
import java.io.InputStream;
import java.util.Properties;

public class PropertyLoader {

	public void Load(){
		Properties prop = new Properties();
		InputStream input = null;

		try {

		      input = getClass().getClassLoader().getResourceAsStream("application.properties");


		    // load a properties file
		    prop.load(input);

		} catch (IOException ex) {
		    ex.printStackTrace();
		} finally {
		    if (input != null) {
		        try {
		            input.close();
		        } catch (IOException e) {
		            e.printStackTrace();
		        }
		    }
		}
	}
}
