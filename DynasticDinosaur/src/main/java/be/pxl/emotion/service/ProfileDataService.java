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
		EntityTransaction tx = em.getTransaction();
		tx.begin();
		Profile pr = em.find(Profile.class, id);
		tx.commit();
		em.close();
		return pr;
	}

	public List<Profile> getProfilesByUserId(int id) {
		EntityManager em = emf.createEntityManager();
		@SuppressWarnings("unchecked")
		List<Profile> profiles = em.createQuery("SELECT p FROM Profile p WHERE p.userId LIKE :uid")
				.setParameter("uid", id).getResultList();

		em.close();
		return profiles;
	}

	public Boolean addProfile(Profile profile) {
		try {
		EntityManager em = emf.createEntityManager();
		EntityTransaction ticket = em.getTransaction();
		ticket.begin();
		em.persist(profile);
		ticket.commit();
		em.close();
		return true;
		} catch (Exception e) {
			return false;
		}
	}

	public Profile updateProfile(Profile profile) {
		EntityManager em = emf.createEntityManager();
		EntityTransaction ticket = em.getTransaction();
		ticket.begin();
		em.merge(profile);
		ticket.commit();
		em.close();
		return profile;
	}

	public void deleteProfile(int id) {
		CommandDataService cs = new CommandDataService();
		ActionDataService as = new ActionDataService();
		Profile p = getProfileById(id);
		Command c = cs.getCommandById(p.getCommands());
		Action a = as.getActionById(p.getActions());
		EntityManager em = emf.createEntityManager();
		EntityTransaction ticket = em.getTransaction();
		ticket.begin();
		em.remove(p);
		em.remove(c);
		em.remove(a);
		ticket.commit();
		em.close();
	}
}
