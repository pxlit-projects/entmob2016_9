package be.pxl.emotion.service;
import javax.persistence.*;

import be.pxl.emotion.bean.Command;

public class CommandDataService {
	private EntityManagerFactory emf = Persistence.createEntityManagerFactory( "dbpu" );
	
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
	
	public Command addCommand(Command command) {
		EntityManager em = emf.createEntityManager();
		EntityTransaction ticket = em.getTransaction();
		ticket.begin();
		em.persist(command);
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
