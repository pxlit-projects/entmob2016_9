package com.tc.maven.data;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;
import java.util.Arrays;
import java.util.List;

import org.springframework.context.ApplicationContext;
import org.springframework.context.support.ClassPathXmlApplicationContext;

import com.fasterxml.jackson.core.JsonGenerationException;
import com.fasterxml.jackson.databind.JsonMappingException;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.tc.maven.bean.Parking;
import com.tc.maven.service.ParkingDataService;

public class ParkingService {
	
	private static final String PARKING_DATA_URL = "http://datatank.stad.gent/4/mobiliteit/bezettingparkingsrealtime.json";
	
	public static void UpdateParkingData(){
		 /*String data = GetData(PARKING_DATA_URL);
		 
		 ObjectMapper mapper = new ObjectMapper();
		 
		 try {
			 List<Parking> parkings = Arrays.asList(mapper.readValue(data, Parking[].class));
			 
			 ApplicationContext ctx = new ClassPathXmlApplicationContext("parkings.xml");
			 ParkingDataService ps = ctx.getBean("parkingService", ParkingDataService.class);
			 
			 for (Parking parking : parkings) {
				 ps.addParking(parking);
			 }
			 
		 } catch (JsonGenerationException e) {
				e.printStackTrace();
		 } catch (JsonMappingException e) {
			e.printStackTrace();
		 } catch (IOException e) {
			e.printStackTrace();
		 }*/
	}
	
	private static String GetData(String urlPath){
		
		String output = "DEFAULT";
		
		/*try {

    		URL url = new URL(urlPath);
    		HttpURLConnection conn = (HttpURLConnection) url.openConnection();
    		conn.setRequestMethod("GET");
    		conn.setRequestProperty("Accept", "application/json");

    		if (conn.getResponseCode() != 200) {
    			throw new RuntimeException("Failed : HTTP error code : "
    					+ conn.getResponseCode());
    		}

    		BufferedReader br = new BufferedReader(new InputStreamReader(
    			(conn.getInputStream())));
    		
    		StringBuilder sb = new StringBuilder();
    		String aux = "";
    		
    		while ((aux = br.readLine()) != null){
    			sb.append(aux);
    		}

    		output = sb.toString();

    		conn.disconnect();

    	  } catch (MalformedURLException e) {

    		e.printStackTrace();

    	  } catch (IOException e) {

    		e.printStackTrace();

    	  }*/
		
		return output;
	}
}
