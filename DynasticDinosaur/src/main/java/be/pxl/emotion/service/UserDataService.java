package be.pxl.emotion.service;

import java.util.List;

import javax.persistence.*;

import org.springframework.stereotype.Service;

import be.pxl.emotion.bean.Profile;
import be.pxl.emotion.bean.User;

@Service("userService")
public class UserDataService {
	private EntityManagerFactory emf = Persistence.createEntityManagerFactory( "dbpu" );
	
	public User getUserById(int id){
		EntityManager em = emf.createEntityManager();
		
		try{
			EntityTransaction tx = em.getTransaction();
			tx.begin();
			User us = em.find(User.class, id);
			tx.commit();
			return us;
		}catch (Exception e) {
			return null;
		} finally {
			em.close();
		}
	}
	
	public User getUserProfileId(String name){
		EntityManager em = emf.createEntityManager();
		User newUser = new User();
		Profile p = new Profile();
		try{
			EntityTransaction tx = em.getTransaction();

			tx.begin();
			Query q = em.createQuery("SELECT u from User u where u.userName =:userNameparam");
			q.setParameter("userNameparam", name);
			User u1 = (User)q.getSingleResult();
			tx.commit();
			
			EntityTransaction tx2 = em.getTransaction();
			tx2.begin();
			Query q2 = em.createQuery("SELECT p from Profile p where p.userId =:userIdparam");
			q2.setParameter("userIdparam", u1.userId);
			p = (Profile)q2.getSingleResult();
			tx2.commit();
			
			EntityTransaction tx3 = em.getTransaction();
			tx3.begin();
			Query query = em.createQuery("UPDATE User u SET u.defaultProfileId =:profileIdParam WHERE u.userName=:usernameParam ");		
			query.setParameter("profileIdParam", p.getProfileId());
            query.setParameter("usernameParam", name);
            query.executeUpdate();
			tx3.commit();
			
			newUser.userName = "";
			newUser.firstName = "";
			newUser.lastName = "";
			newUser.userId = 0;
			newUser.password = "";
			newUser.defaultProfileId = p.getProfileId();
			return newUser;
		}catch (Exception e){
			System.out.println("1: "+ e.getMessage());
			return null;
		}finally {
			em.close();
		}
	}
	
	public User getUserByName(String name){
		EntityManager em = emf.createEntityManager();

		try{
			EntityTransaction tx = em.getTransaction();
			tx.begin();
			Query q = em.createQuery("SELECT u from User u where u.userName LIKE '"  + name + "'");
			
			User us = (User)q.getSingleResult();
			tx.commit();
			
			User newUser = new User();
			newUser.userName = us.userName;
			newUser.firstName = "";
			newUser.lastName = "";
			newUser.userId = us.userId;
			newUser.password = "";
			newUser.defaultProfileId = 0;
			
			return newUser;
		}catch (Exception e){
			
			System.out.println(e.getMessage());
			return null;
		}finally {
			em.close();
		}
	}
	
	public List<User> getAllUsers(){
		EntityManager em = emf.createEntityManager();
		
		try{
			Query q = em.createQuery("SELECT e from User e");
			@SuppressWarnings("unchecked")
			List<User> users = (List<User>)q.getResultList();
			
			return users;
		}catch (Exception e) {
			// TODO: handle exception
			return null;
		}finally {
			em.close();
		}
		
		
	}

	public User addUser(User user) {
		EntityManager em = emf.createEntityManager();
		
		try{
			EntityTransaction ticket = em.getTransaction();
			ticket.begin();
			em.persist(user);
			ticket.commit();
			return user;
		}catch (Exception e) {
			// TODO: handle exception
			System.out.println(e.getMessage());
			return null;
		}finally {
			em.close();
		}
		
	}

	public User updateUser(User user) {
		EntityManager em = emf.createEntityManager();
		
		try{
			EntityTransaction ticket = em.getTransaction();
			ticket.begin();
			em.merge(user);
			ticket.commit();
			return user;
		}catch (Exception e) {
			// TODO: handle exception
			return null;
		}finally {
			em.close();
		}
		
	}

	public void deleteUser(int id) {
		EntityManager em = emf.createEntityManager();
		try{

			User u = getUserById(id);
			EntityTransaction ticket = em.getTransaction();
			ticket.begin();
			em.remove(u);
			ticket.commit();
		}catch (Exception e) {
			// TODO: handle exception
		} finally {
			em.close();
		}
	}
}
