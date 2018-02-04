#!/bin/bash

BIN_MARKDOWNPDF=/usr/local/bin/markdown-pdf

DIR_WORK=`dirname $0`
DIR_TEMP_FILES=$(DIR_WORK)/temp
DIR_TEMP_DOC=$(DIR_WORK)/temp-doc

APP_NAME=$1
APP_VERSION=$2

# 古いファイルを削除
rm -Rf $(DIR_TEMP_FILES)/*
rm -Rf $(DIR_TEMP_DOC)/*

mkdir -p $(DIR_TEMP_FILES)
mkdir -p $(DIR_TEMP_DOC)

# +++++++++++++++++++++++++++++++++++++
# 関連ファイルを収集
# +++++++++++++++++++++++++++++++++++++

# 本体
cp -Rf "$(DIR_WORK)../build/release/*" "$(DIR_TEMP)/"

# ドキュメント
cp -Rf "$(DIR_WORK)../README.md" $()
cp -Rf "$(DIR_WORK)../doc"

