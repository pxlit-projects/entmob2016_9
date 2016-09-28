package com.tc.maven.service;

import java.util.ArrayList;
import java.util.List;

import javax.persistence.EntityManager;
import javax.persistence.EntityManagerFactory;
import javax.persistence.EntityTransaction;
import javax.persistence.PersistenceUnit;
import javax.persistence.Query;

import org.springframework.stereotype.Service;

import com.tc.maven.bean.Parking;

@Service("parkingService")
public class ParkingDataService {
	private EntityManagerFactory emf;
	
	@PersistenceUnit
	public void setEntityManagerFactory(EntityManagerFactory emf){
		this.emf = emf;
	}
	
	public Parking getParkingById(int id){
		EntityManager em = emf.createEntityManager();
		EntityTransaction tx = em.getTransaction();
		tx.begin();
		Parking parking = em.find(Parking.class, id);
		tx.commit();
		em.close();
		return parking;
	}
	
	public List<Parking> getAllParkings(){
		/*EntityManager em = emf.createEntityManager();
		
		Query q = em.createQuery("SELECT e from Parking e");
		//List<Parking> parkings = (List<Parking>)q.getResultList();*/
		List<Parking> parkings = new ArrayList<Parking>();
		
		Parking p = new Parking();
		p.setAddress("gaarstraat 2");
		p.setId(1);
		p.setContactInfo("nee");
		p.setDescription("een parking");
		
		parkings.add(p);
		
		//em.close();
		return parkings;
	}
	
	public void addParking(Parking parking){
		EntityManager em = emf.createEntityManager();
		EntityTransaction tx = em.getTransaction();
		tx.begin();
		em.persist(parking);
		tx.commit();
		em.close();
	}
}
