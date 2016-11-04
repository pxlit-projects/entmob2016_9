package be.pxl.emotion.service;

import java.util.List;

import javax.persistence.*;

import org.springframework.stereotype.Service;

import be.pxl.emotion.bean.User;

@Service("userService")
public class UserDataService {
	private EntityManagerFactory emf = Persistence.createEntityManagerFactory( "dbpu" );
	
	public User getUserById(int id){
		EntityManager em = emf.createEntityManager();
		EntityTransaction tx = em.getTransaction();
		tx.begin();
		User us = em.find(User.class, id);
		tx.commit();
		em.close();
		return us;
	}
	
	public User getUserByName(String name){
		
		try{
			EntityManager em = emf.createEntityManager();
			EntityTransaction tx = em.getTransaction();
			tx.begin();
			Query q = em.createQuery("SELECT u from User u where u.userName LIKE '"  + name + "'");
			User us = (User)q.getSingleResult();
			tx.commit();
			em.close();
			
			User newUser = new User();
			newUser.userName = us.userName;
			newUser.firstName = "";
			newUser.lastName = "";
			newUser.userId = us.userId;
			newUser.password = "";
			
			return newUser;
		}catch (Exception e){
			return null;
		}
	}
	
	public List<User> getAllUsers(){
		EntityManager em = emf.createEntityManager();
		
		Query q = em.createQuery("SELECT e from User e");
		@SuppressWarnings("unchecked")
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
