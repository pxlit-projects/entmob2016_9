package be.pxl.emotion.service;

import java.util.List;
import javax.persistence.EntityManager;
import javax.persistence.EntityManagerFactory;
import javax.persistence.EntityTransaction;
import javax.persistence.Persistence;
import javax.persistence.PersistenceContext;
import javax.persistence.PersistenceUnit;
import org.springframework.stereotype.Service;

import be.pxl.emotion.bean.Action;
import be.pxl.emotion.bean.Command;
import be.pxl.emotion.bean.Profile;

@Service("profileService")
public class ProfileDataService {
	private EntityManagerFactory emf = Persistence.createEntityManagerFactory( "dbpu" );

	public Profile getProfileById(int id) {
		EntityManager em = emf.createEntityManager();
		
		try{
			EntityTransaction tx = em.getTransaction();
			tx.begin();
			Profile pr = em.find(Profile.class, id);
			tx.commit();
			return pr;

		}catch (Exception e) {
			// TODO: handle exception
			return null;
		}finally {
			em.close();

		}
		
	}

	public List<Profile> getProfilesByUserId(int id) {
		EntityManager em = emf.createEntityManager();
		
		try{
			@SuppressWarnings("unchecked")
			List<Profile> profiles = em.createQuery("SELECT p FROM Profile p WHERE p.userId LIKE :uid")
					.setParameter("uid", id).getResultList();

			return profiles;
		}catch (Exception e) {
			// TODO: handle exception
			return null;
		}finally {
			em.close();
		}
		
	}

	public Profile addProfile(Profile profile) {
		EntityManager em = emf.createEntityManager();

		try {
			EntityTransaction ticket = em.getTransaction();
			ticket.begin();
			em.persist(profile);
			ticket.commit();
			return profile;
		} catch (Exception e) {
			return null;
		}finally{
			em.close();
		}
	}

	public Profile updateProfile(Profile profile) {
		EntityManager em = emf.createEntityManager();
		
		try{

			EntityTransaction ticket = em.getTransaction();
			ticket.begin();
			em.merge(profile);
			ticket.commit();
			return profile;
		}catch (Exception e) {
			// TODO: handle exception
			return null;
		} finally{
			em.close();
		}
	}

	public void deleteProfile(int id) {
		
		EntityManager em = emf.createEntityManager();
		
		try{
			CommandDataService cs = new CommandDataService();
			ActionDataService as = new ActionDataService();
			Profile p = getProfileById(id);
			
			EntityTransaction ticket = em.getTransaction();
			ticket.begin();
			em.remove(p);
			ticket.commit();
		}catch (Exception e) {
			// TODO: handle exception
		}finally {
			em.close();

		}
		
		
	}
}
