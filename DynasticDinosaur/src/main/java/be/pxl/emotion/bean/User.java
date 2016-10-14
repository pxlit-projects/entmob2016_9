package be.pxl.emotion.bean;

import java.io.UnsupportedEncodingException;
import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;

public class User {
	public int userId;
	public String firstName;
	public String lastName;
	private String password;

	public boolean setPass(String toSet, String prev) {
		if (password.equals(prev)) {
			password = toSet;
			return true;
		}
		return false;
	}

	public boolean checkPass(String toCheck) {
		return password.equals(toCheck);
	}

	@SuppressWarnings("finally")
	public String encrypt(String toEncrypt) {
		MessageDigest md;
		String ret = "";
		toEncrypt += userId;
		try {
			md = MessageDigest.getInstance("SHA-256");

			md.update(toEncrypt.getBytes("UTF-8"));
			
			byte[] digest = md.digest();
			ret = new String(digest);
		} catch (NoSuchAlgorithmException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} catch (UnsupportedEncodingException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} finally {
			return ret;
		}
	}
}
