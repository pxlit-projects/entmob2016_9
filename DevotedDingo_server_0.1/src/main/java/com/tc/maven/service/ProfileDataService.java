package com.tc.maven.service;

import java.util.List;
import javax.persistence.EntityManager;
import javax.persistence.EntityManagerFactory;
import javax.persistence.EntityTransaction;
import javax.persistence.PersistenceUnit;
import javax.persistence.Query;
import org.springframework.stereotype.Service;

import com.tc.maven.bean.*;

@Service("profileService")
public class ProfileDataService {
	private EntityManagerFactory emf;

	@PersistenceUnit
	public void setEntityManagerFactory(EntityManagerFactory emf) {
		this.emf = emf;
	}

	public Profile getUserById(int id) {
		EntityManager em = emf.createEntityManager();
		EntityTransaction tx = em.getTransaction();
		tx.begin();
		Profile pr = em.find(Profile.class, id);
		tx.commit();
		em.close();
		return pr;
	}

	public List<Profile> getProfilesByUserId(int id) {
		EntityManager em = emf.createEntityManager();
		List<Profile> users = em.createQuery("SELECT p FROM Profile p WHERE p.userId LIKE :uid").setParameter("uid", id)
				.getResultList();

		em.close();
		return users;
	}

	public Profile addProfile(Profile profile) {
		EntityManager em = emf.createEntityManager();
		EntityTransaction ticket = em.getTransaction();
		ticket.begin();
		em.persist(profile);
		ticket.commit();
		em.close();
		return profile;
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
