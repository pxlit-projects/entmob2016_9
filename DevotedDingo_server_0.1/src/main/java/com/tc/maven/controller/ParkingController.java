package com.tc.maven.controller;

import java.util.List;  
import com.tc.maven.bean.Parking;
import com.tc.maven.service.ParkingDataService;

import org.springframework.web.bind.annotation.PathVariable;  
import org.springframework.web.bind.annotation.RequestBody;  
import org.springframework.web.bind.annotation.RequestMapping;  
import org.springframework.web.bind.annotation.RequestMethod;  
import org.springframework.web.bind.annotation.RestController;  
  
@RestController  
public class ParkingController {  
  
 ParkingDataService parkingService = new ParkingDataService();  
  
 @RequestMapping(value = "/parkings", method = RequestMethod.GET, headers = "Accept=application/json")  
 public List<Parking> getCountries() {  
  List<Parking> listOfCountries = parkingService.getAllParkings();  
  return listOfCountries;  
 }  
  
 @RequestMapping(value = "/parking/{id}", method = RequestMethod.GET, headers = "Accept=application/json")  
 public Parking getParkingById(@PathVariable int id) {  
  return parkingService.getParkingById(id);  
 }  
  
 /*@RequestMapping(value = "/parkings", method = RequestMethod.POST, headers = "Accept=application/json")  
 public Country addCountry(@RequestBody Country country) {  
  return countryService.addCountry(country);  
 }  
  
 @RequestMapping(value = "/countries", method = RequestMethod.PUT, headers = "Accept=application/json")  
 public Country updateCountry(@RequestBody Country country) {  
  return countryService.updateCountry(country);  
  
 }  
  
 @RequestMapping(value = "/country/{id}", method = RequestMethod.DELETE, headers = "Accept=application/json")  
 public void deleteCountry(@PathVariable("id") int id) {  
  countryService.deleteCountry(id);  
  
 }   */
}
