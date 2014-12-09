class Walkabout {
	void visit(Object v) {
		if (v != null) {
			Object[] os = { v };
			Class<?> vClass = v.getClass();
			Class<?>[] cs = { vClass };
			try {
				this.getClass().getMethod("visit", cs).invoke(this, os);
			} catch (java.lang.NoSuchMethodException e) {
				if (!((v instanceof Number) | (v instanceof Byte)
						| (v instanceof Short) | (v instanceof Character) | (v instanceof Boolean))) {
					java.lang.reflect.Field[] vFields = vClass.getFields();
					for (int i = 0; i < vFields.length; i++) {
						try {
							this.visit(vFields[i].get(v));
						} catch (java.lang.IllegalAccessException e1) {
							;
						}
						// Cannot happen.
					}
				}
			} catch (java.lang.Exception e) {
				;
			} // Cannot happen.
		}
	}
}