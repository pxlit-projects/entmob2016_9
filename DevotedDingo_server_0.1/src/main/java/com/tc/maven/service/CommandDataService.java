package com.tc.maven.service;
import java.util.ArrayList;
import java.util.List;

import javax.persistence.*;

import org.springframework.stereotype.Service;

import com.tc.maven.bean.Command;
import com.tc.maven.bean.Profile;
import com.tc.maven.bean.User;

public class CommandDataService {
	private EntityManagerFactory emf;
	
	//Database interactie word hier geregeld
	// entitymanagefactory is de basis van de connectie met de database
	@PersistenceUnit
	public void setEntityManagerFactory(EntityManagerFactory emf) {
		this.emf = emf;
	}
	
	public Command getCommandById(int id)
	{
		EntityManager em = emf.createEntityManager();
		EntityTransaction tx = em.getTransaction();
		tx.begin();
		Command cm = em.find(Command.class, id);
		tx.commit();
		em.close();
		return cm;
	}
	
	public Command updateCommand(Command command) 
	{
		EntityManager em = emf.createEntityManager();
		EntityTransaction ticket = em.getTransaction();
		ticket.begin();
		em.merge(command);
		ticket.commit();
		em.close();
		return command;
	}
	
	
	public void deleteCommand(int id) {
		EntityManager em = emf.createEntityManager();
		Command c = getCommandById(id);
		EntityTransaction ticket = em.getTransaction();
		ticket.begin();
		em.remove(c);
		ticket.commit();
		em.close();
	}
}
