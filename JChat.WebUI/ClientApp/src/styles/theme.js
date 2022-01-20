import { createDarkTheme } from 'baseui';

const THEME = {
  headerSize: '50px',
  padding: '15px',
  borderRadius: '7.5px',
  inputPadding: '3px 7px',
  textBreak: {
    // whiteSpace: 'pre-warp',
    whiteSpace: 'nowrap',
    overflowWrap: 'break-word',
    overflow: 'hidden',
    textOverflow: 'ellipsis',
  },
  colors: {
    themePrimary: '#5d00ff',
    themeLighterAlt: '#f9f5ff',
    themeLighter: '#e5d6ff',
    themeLight: '#cfb3ff',
    themeTertiary: '#9e66ff',
    themeSecondary: '#711fff',
    themeDarkAlt: '#5400e6',
    themeDark: '#4700c2',
    themeDarker: '#34008f',
    accentOrange: '#ff6f00',
    accent: '#ff6f00',
    accentGreen: '#a2ff00',
    accentPink: '#ff00a2',
    neutralLighterAlt: '#faf9f8',
    neutralLighter: '#f3f2f1',
    neutralLight: '#edebe9',
    neutralQuaternaryAlt: '#e1dfdd',
    neutralQuaternary: '#d0d0d0',
    neutralTertiaryAlt: '#c8c6c4',
    neutralTertiary: '#a19f9d',
    neutralSecondary: '#605e5c',
    neutralPrimaryAlt: '#3b3a39',
    neutralPrimary: '#323130',
    neutralDark: '#201f1e',
    black: '#000000',
    green: '#a2ff00',
    red: '#ff2f00',
    white: '#ffffff',
    b2: '#121212',
    b3: '#181818',
  },
  shadow1: '0 1px 3px rgba(0,0,0,0.12), 0 1px 2px rgba(0,0,0,0.24)',
  shadow2: '0 3px 6px rgba(0,0,0,0.16), 0 3px 6px rgba(0,0,0,0.23)',
  shadow3: '0 10px 20px rgba(0,0,0,0.19), 0 6px 6px rgba(0,0,0,0.23)',
  shadow3inverted: '0 -10px 20px rgba(0,0,0,0.19), 0 -6px 6px rgba(0,0,0,0.23)',
  shadow4: '0 14px 28px rgba(0,0,0,0.25), 0 10px 10px rgba(0,0,0,0.22)',
  shadow5: '0 19px 38px rgba(0,0,0,0.30), 0 15px 12px rgba(0,0,0,0.22)',
};

THEME.incomingMessage = {
  border: `1px solid ${THEME.colors.neutralDark}22`,
  backgroundColor: THEME.colors.neutralPrimaryAlt,
};

THEME.outgoingMessage = {
  // border: `1px solid ${THEME.colors.accent}55`,
  backgroundColor: `${THEME.colors.themePrimary}77`,
};

THEME.colors.neutralPrimaryAlpha = `${THEME.colors.neutralPrimary}77`;

// https://baseweb.design/guides/theming/
const baseUiPrimitives = {
  accent: THEME.colors.accentGreen,
};
const baseUiOverrides = {
  colors: {
    borderFocus: THEME.colors.themeTertiary,
  },
};

const theme = createDarkTheme(baseUiPrimitives, baseUiOverrides);

theme.colors = {
  ...theme.colors,
  ...THEME.colors,
};

const { colors, ...themeProps } = THEME;

export const baseUiTheme = {
  ...theme,
  ...themeProps,
};

export default THEME;
