package com.tc.maven.service;

import javax.persistence.EntityManager;
import javax.persistence.EntityManagerFactory;
import javax.persistence.EntityTransaction;
import javax.persistence.PersistenceUnit;

import com.tc.maven.bean.Action;

public class ActionDataService {
private EntityManagerFactory emf;
	
	//Database interactie word hier geregeld
	// entitymanagefactory is de basis van de connectie met de database
	@PersistenceUnit
	public void setEntityManagerFactory(EntityManagerFactory emf) {
		this.emf = emf;
	}

	public Action addAction(Action action) 
	{
		EntityManager em = emf.createEntityManager();
		EntityTransaction ticket = em.getTransaction();
		ticket.begin();
		em.persist(action);
		ticket.commit();
		em.close();
		return action;
	}
	
	public Action getActionById(int id)
	{
		EntityManager em = emf.createEntityManager();
		EntityTransaction tx = em.getTransaction();
		tx.begin();
		Action cm = em.find(Action.class, id);
		tx.commit();
		em.close();
		return cm;
	}
	
	public Action updateAction(Action action) 
	{
		EntityManager em = emf.createEntityManager();
		EntityTransaction ticket = em.getTransaction();
		ticket.begin();
		em.merge(action);
		ticket.commit();
		em.close();
		return action;
	}
	
	
	public void deleteAction(int id) {
		EntityManager em = emf.createEntityManager();
		Action c = getActionById(id);
		EntityTransaction ticket = em.getTransaction();
		ticket.begin();
		em.remove(c);
		ticket.commit();
		em.close();
	}
}
