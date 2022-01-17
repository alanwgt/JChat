export const dotAccess = (obj, path, def = null) => {
  if (!obj) return def;
  if (!Object.keys(obj).length) return def;

  const parts = path.split('.');
  let pointer = obj;

  parts.forEach((p) => {
    if (!pointer[p]) {
      return;
    }

    pointer = pointer[p];
  });

  return pointer || def;
};
