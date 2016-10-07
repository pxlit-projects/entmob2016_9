package com.tc.maven.service;
import java.util.ArrayList;
import java.util.List;

import javax.persistence.*;

import org.springframework.stereotype.Service;

import com.tc.maven.bean.Command;
import com.tc.maven.bean.Profile;

public class CommandDataService {
	private EntityManagerFactory emf;
	
	//Database interactie word hier geregeld
	// entitymanagefactory is de basis van de connectie met de database
	@PersistenceUnit
	public void setEntityManagerFactory(EntityManagerFactory emf) {
		this.emf = emf;
	}
	
	public Command getCommands(int id)
	{
		EntityManager em = emf.createEntityManager();
		EntityTransaction tx = em.getTransaction();
		tx.begin();
		Command cm = em.find(Command.class, id);
		tx.commit();
		em.close();
		return cm;
	}
}
