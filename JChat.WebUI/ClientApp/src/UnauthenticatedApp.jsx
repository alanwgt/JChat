import React from 'react';
import { useTranslation } from 'react-i18next';

import KratosResponseForm from '@/components/kratos/KratosResponseForm';
import waitForPromise from '@/utils/waitForPromise';
import {
  beginLoginFlow,
  beginRegistrationFlow,
  submitLogin,
  submitRegistration,
} from '@/services/auth.service';
import ChooseAuth, { AuthAction } from '@/components/auth/ChooseAuth';
import { useAuth } from '@/providers/authContext';
import feedbackUtils from '@/utils/feedback.utils';
import HackedText from '@/components/typography/HackedText';

const KratosUiForm = waitForPromise(KratosResponseForm);

const UnauthenticatedApp = () => {
  const [t] = useTranslation();
  const { login } = useAuth();

  const [currentAction, setCurrentAction] = React.useState(0);
  const [kratosData, setKratosData] = React.useState(null);

  const isActionSelected = () =>
    Object.values(AuthAction).includes(currentAction);

  React.useEffect(() => {
    if (currentAction === AuthAction.SignUp && kratosData === null) {
      setKratosData(beginRegistrationFlow());
    } else if (currentAction === AuthAction.SignIn && kratosData === null) {
      setKratosData(beginLoginFlow());
    }
  }, [currentAction]);

  if (!isActionSelected()) {
    return <ChooseAuth onSelect={setCurrentAction} />;
  }

  if (currentAction === AuthAction.SignIn) {
    return (
      <KratosUiForm
        title={<HackedText>{t('auth.login')}</HackedText>}
        waitFor={kratosData}
        onCancel={() => {
          setCurrentAction(0);
          setKratosData(null);
        }}
        onSubmit={(flowId, data) => {
          submitLogin(flowId, data)
            .then(({ data: sessionData }) => {
              login(sessionData.session);
            })
            .catch((error) => {
              setKratosData(error.response);
            });
        }}
      />
    );
  }

  return (
    <KratosUiForm
      title={<HackedText>{t('auth.register')}</HackedText>}
      waitFor={kratosData}
      onCancel={() => {
        setCurrentAction(0);
        setKratosData(null);
      }}
      onSubmit={(flowId, data) => {
        submitRegistration(flowId, data)
          .then(({ data: userData }) => {
            feedbackUtils.positive(t('auth.account_registered'));
            login(userData);
          })
          .catch((error) => {
            setKratosData(error.response);
          });
      }}
    />
  );
};

export default UnauthenticatedApp;
