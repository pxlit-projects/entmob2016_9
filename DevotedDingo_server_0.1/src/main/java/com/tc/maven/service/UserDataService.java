package com.tc.maven.service;

import java.util.ArrayList;
import java.util.List;

import javax.persistence.*;

import org.springframework.stereotype.Service;

import com.tc.maven.bean.User;

@Service("userService")
public class UserDataService {
	private EntityManagerFactory emf;
	
	@PersistenceUnit
	public void setEntityManagerFactory(EntityManagerFactory emf){
		this.emf = emf;
	}
	
	public User getUserById(int id){
		EntityManager em = emf.createEntityManager();
		EntityTransaction tx = em.getTransaction();
		tx.begin();
		User us = em.find(User.class, id);
		tx.commit();
		em.close();
		return us;
	}
	
	public List<User> getAllUsers(){
		EntityManager em = emf.createEntityManager();
		
		Query q = em.createQuery("SELECT e from User e");
		List<User> users = (List<User>)q.getResultList();
		
		em.close();
		return users;
	}

	public User addUser(User user) {
		EntityManager em = emf.createEntityManager();
		EntityTransaction ticket = em.getTransaction();
		ticket.begin();
		em.persist(user);
		ticket.commit();
		em.close();
		return user;
	}

	public User updateUser(User user) {
		EntityManager em = emf.createEntityManager();
		EntityTransaction ticket = em.getTransaction();
		ticket.begin();
		em.merge(user);
		ticket.commit();
		em.close();
		return user;
	}

	public void deleteUser(int id) {
		EntityManager em = emf.createEntityManager();
		User u = getUserById(id);
		EntityTransaction ticket = em.getTransaction();
		ticket.begin();
		em.remove(u);
		ticket.commit();
		em.close();
	}
}
