import { V0alpha2Api } from '@ory/kratos-client';
import { axios } from '@/services/api';

export const AuthUrl = 'http://kratos.jchat.alanwgt.com';

const kratosInstance = new V0alpha2Api({}, AuthUrl, axios);

export const beginRegistrationFlow = () =>
  kratosInstance.initializeSelfServiceRegistrationFlowForBrowsers();

export const submitRegistration = (
  flow,
  { csrf_token: csrfToken, method, password, traits }
) =>
  kratosInstance.submitSelfServiceRegistrationFlow(flow, {
    csrf_token: csrfToken,
    method,
    password,
    provider: 'string',
    traits,
  });

export const beginLoginFlow = () =>
  kratosInstance.initializeSelfServiceLoginFlowForBrowsers();

export const submitLogin = (
  flow,
  {
    csrf_token: csrfToken,
    method,
    password,
    password_identifier: passwordIdentifier,
  }
) =>
  kratosInstance.submitSelfServiceLoginFlow(flow, null, {
    csrf_token: csrfToken,
    method,
    password,
    password_identifier: passwordIdentifier,
  });

export const checkAuthStatus = () => kratosInstance.toSession();

export const logout = () =>
  kratosInstance
    .createSelfServiceLogoutFlowUrlForBrowsers()
    .then(({ data: { logout_token: token } }) =>
      kratosInstance.submitSelfServiceLogoutFlow(token)
    );
